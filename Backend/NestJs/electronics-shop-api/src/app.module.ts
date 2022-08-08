import { CacheModule, Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UsersModule } from './users/users.module';
import { ProductsModule } from './products/products.module';
import { OrdersModule } from './orders/orders.module';
import { AuthModule } from './auth/auth.module';
import { NotificationsModule } from './notifications/notifications.module';
import * as redisStore from 'cache-manager-redis-store';

@Module({
  imports: [UsersModule, 
    MongooseModule.forRoot('mongodb://localhost:27017/electronics-shop'), 
    CacheModule.register({
      store: redisStore,
      host: 'localhost',
      port: 5003,
      ttl: 60, max: 100, isGlobal:true
    }),
    ProductsModule, OrdersModule, AuthModule, NotificationsModule
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
