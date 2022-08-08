import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model } from 'mongoose';
import { CreateProductDto } from './dto/create-product.dto';
import { UpdateProductDto } from './dto/update-product.dto';
import { Product } from './entities/product.entity';

@Injectable()
export class ProductsService {
  constructor(
    @InjectModel("Product") private readonly productModel: Model<Product>
  ) { }
  create(createProductDto: CreateProductDto) {
    return this.productModel.create(createProductDto);
  }

  findAll() {
    return this.productModel.find();
  }

  findOne(id: string) {
    return this.productModel.findOne({ _id: id });
  }

  update(id: string, updateProductDto: UpdateProductDto) {
    return this.productModel.updateOne({ _id: id }, updateProductDto);
  }

  remove(id: string) {
    return this.productModel.deleteOne({ _id: id });
  }
  
}
