import { WebSocketGateway,WebSocketServer, SubscribeMessage, MessageBody,ConnectedSocket } from '@nestjs/websockets';
import { NotificationsService } from './notifications.service';
import { CreateNotificationDto } from './dto/create-notification.dto';
import { UpdateNotificationDto } from './dto/update-notification.dto';
import { Socket,Server } from 'socket.io';

@WebSocketGateway({ cors: { origin: '*' } })
export class NotificationsGateway {
  @WebSocketServer()
  server: Server;
  constructor(private readonly notificationsService: NotificationsService) {}
  

  @SubscribeMessage('createNotification')
  create(@MessageBody() createNotificationDto: CreateNotificationDto) {
    return this.notificationsService.create(createNotificationDto);
  }

  @SubscribeMessage('findAllNotifications')
  findAll() {
    return this.notificationsService.findAll();
  }

  @SubscribeMessage('findOneNotification')
  findOne(@MessageBody() id: number) {
    return this.notificationsService.findOne(id);
  }

  @SubscribeMessage('updateNotification')
  update(@MessageBody() updateNotificationDto: UpdateNotificationDto) {
    return this.notificationsService.update(updateNotificationDto.id, updateNotificationDto);
  }

  @SubscribeMessage('removeNotification')
  remove(@MessageBody() id: number) {
    return this.notificationsService.remove(id);
  }

  
  sendNotificationOrderDelivered(userId : string) {
    console.log(`Sending notification to ${userId}`);
    console.log(userId)
    this.server.to(userId).emit('orderdelivery', "Order Delivered");
  }

  @SubscribeMessage("join")
  handleJoin(@ConnectedSocket() client: Socket, @MessageBody() data: any) {
    //console.log(client)
    console.log(`Client with id ${data} joined`);
    client.join(data)
    console.log(data)
    this.server.to(data).emit('notification', `hello in room ${data}`);
  }
  @SubscribeMessage("connect")
  handleConnect(client: Socket) {
    console.log(`Client with id ${client.id} connected`);
  }
  @SubscribeMessage("connection")
  handleConnection(client: Socket) {
    console.log(`Client with id ${client.id} is connecting`);
    client.emit('join', "join room");
    //client.join("room1");
    //this.server.to("room1").emit('notification', "hello in room");
    return '"connection"';
  }
}
