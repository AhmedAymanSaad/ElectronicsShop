import { CACHE_MANAGER, ExecutionContext, Inject, Injectable } from '@nestjs/common';
import { REQUEST } from '@nestjs/core';
import { InjectModel } from '@nestjs/mongoose';
import { create } from 'domain';
import { Model } from 'mongoose';
import { async } from 'rxjs';
import { ProductsService } from 'src/products/products.service';
import { UsersService } from 'src/users/users.service';
import { CreateOrderDto } from './dto/create-order.dto';
import { UpdateOrderDto } from './dto/update-order.dto';
import { Order } from './entities/order.entity';
import jwt_decode from 'jwt-decode';
import { BESTSELLER_COUNT } from 'src/common/constants';
import { Cache } from 'cache-manager';
import { NotificationsGateway } from 'src/notifications/notifications.gateway';

@Injectable()
export class OrdersService {
  constructor(@InjectModel("Order") private readonly orderModel: Model<Order>, private readonly productsService: ProductsService,
  @Inject(REQUEST) private  request, @Inject(CACHE_MANAGER) private readonly cacheManager: Cache,
  private readonly noftificationsGateway: NotificationsGateway) {
    /*
    const authToken = request.headers["authorization"].split(" ")[1]
    console.log(authToken);
    const pl = jwt_decode(authToken) as any;
    console.log((jwt_decode(request.headers["authorization"].split(" ")[1])as any).user.userId);
    console.log("==========================");
    */
  }
  
  //IMPORTANNTTTTTTTTTTTTTTTTTTTTT
  async create(createOrderDto: CreateOrderDto) {
    createOrderDto.userId = (jwt_decode(this.request.headers["authorization"].split(" ")[1])as any).user.userId
    createOrderDto.price = (await this.productsService.findOne(createOrderDto.productId)).price * createOrderDto.quantity;
    return this.orderModel.create(createOrderDto);
  }

  findAll() {
    return `This action returns all orders`;
  }

  findOne(id: number) {
    return `This action returns a #${id} order`;
  }

  update(id: number, updateOrderDto: UpdateOrderDto) {
    return `This action updates a #${id} order`;
  }

  remove(id: number) {
    return `This action removes a #${id} order`;
  }
  async findBestsellers() {
    /*
    const cache = await this.cacheManager.get('bestsellers');
    if (cache) {
      console.log('cache used');
      return cache;}
    console.log("cache not found");
    */
    const allProducts = this.orderModel.aggregate([
      {
        $group:
        {
          _id: "$productId",
          totalAmount: { $sum: "$quantity" },
        }
      },
      {
        $sort: { totalAmount: -1 }
      },
      {
        $limit: BESTSELLER_COUNT
      },
      {
        $lookup: {
          from: "products",
          localField: "_id",
          foreignField: "_id",
          as: "product"
        }
      }]).exec();
    //await this.cacheManager.set("bestsellers", allProducts, { ttl: 60 * 60 * 24 });
    //console.log("cache set");
    console.log("bestsellers");
    return allProducts;
  }

  async updateStatusDelivered(id: string) {
    const res = await this.orderModel.findByIdAndUpdate(id, { status: "delivered" });
    const orderup = await this.orderModel.findById(id).exec();
    //const ord = orderup.toObject();
    //console.log(orderup.userId.toString());
    this.noftificationsGateway.sendNotificationOrderDelivered(orderup.userId.toString());
    console.log("order delivered");
    return orderup;
  }

  async findPending() {
    const res = await this.orderModel.find({ status: "pending" }).exec();
    return res;
  }
} 
