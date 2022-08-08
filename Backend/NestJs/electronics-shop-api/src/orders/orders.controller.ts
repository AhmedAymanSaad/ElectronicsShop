import { Controller, Get, Post, Body, Patch, Param, Delete, UseGuards, CacheInterceptor, CacheKey, UseInterceptors } from '@nestjs/common';
import { OrdersService } from './orders.service';
import { CreateOrderDto } from './dto/create-order.dto';
import { UpdateOrderDto } from './dto/update-order.dto';
import { Roles } from 'src/common/roles.decorator';
import { JwtGuard } from 'src/auth/guards/jwt.guard';
import { RolesGuard } from 'src/auth/guards/roles.guard';
import { Role } from 'src/common/roles.enum';

@Controller('orders')
export class OrdersController {
  constructor(private readonly ordersService: OrdersService) {}

  @Post()
  @Roles(Role.User)
  @UseGuards(JwtGuard, RolesGuard)
  create(@Body() createOrderDto: CreateOrderDto) {
    return this.ordersService.create(createOrderDto);
  }

  @Get()
  findAll() {
    return this.ordersService.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.ordersService.findOne(+id);
  }

  @Patch(':id')
  update(@Param('id') id: string, @Body() updateOrderDto: UpdateOrderDto) {
    return this.ordersService.update(+id, updateOrderDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.ordersService.remove(+id);
  }
  @Get('/products/bestsellers')
  @UseInterceptors(CacheInterceptor)
  @CacheKey('b')
  findBestsellers() {
    return this.ordersService.findBestsellers();
  }
  @Patch('/status/:id')
  @Roles(Role.Delivery)
  @UseGuards(JwtGuard, RolesGuard)
  updateStatusDelivered(@Param('id') id: string) {
    return this.ordersService.updateStatusDelivered(id);
  }
}
