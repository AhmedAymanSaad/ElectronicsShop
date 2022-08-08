import { Inject, Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
import { User } from './entities/user.entity';
import { Model } from 'mongoose';
import { AuthUserDto } from './dto/auth-user.dto';
import { REQUEST } from '@nestjs/core';
import jwt_decode from 'jwt-decode';

@Injectable()
export class UsersService {
  constructor(
    @InjectModel("User") private readonly userModel: Model<User>, @Inject(REQUEST) private request) {
      /*
    const authToken = request.headers["authorization"].split(" ")[1]
    console.log(authToken);
    const pl = jwt_decode(authToken) as any;
    console.log((jwt_decode(request.headers["authorization"].split(" ")[1])as any).user.userId);
    console.log("==========================");
    */
  }

  _mapAuthUserDto(user: any): AuthUserDto {
    const authToken = null;
    const roles = null;
    try{
      const authToken = user.authToken;
      const roles = user.roles;
    }catch(err){
      const authToken = null;
      const roles = null;
    }

    return {
      userId: user.id,
      authToken: authToken,
      roles:roles
    };
  }
  
  async create(createUserDto: CreateUserDto) {
    try{
      const user = new this.userModel(createUserDto);
      const result = await user.save();
      return result;
    }
    catch(err){
    }
  }
  async findOne(id: string) {
    return this.userModel.findOne({_id: id});
  }
  async findOneByEmail(email: string) {
    return this.userModel.findOne({email: email});
  }

  async hasRoles(id: string, roles: string[]) : Promise<boolean> {
    const user = await this.userModel.findOne({_id: id});
    if (!user)
      return false;
    return roles.every(role => user.roles.includes(role));
  }

}
