import { Controller, Get, Post, Body, Patch, Param, Delete, UseGuards } from '@nestjs/common';
import { UsersService } from './users.service';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
import { Router } from 'express';
import { LoginUserDto } from './dto/login-user.dto';
import { AuthUserDto } from './dto/auth-user.dto';
import { JwtGuard } from 'src/auth/guards/jwt.guard';
import { Roles } from 'src/common/roles.decorator';
import { Role } from 'src/common/roles.enum';
import { RolesGuard } from 'src/auth/guards/roles.guard';

@Controller('users')
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @Get(':id')
  @Roles(Role.Admin)
  @UseGuards(JwtGuard, RolesGuard)
  findOne(@Param('id') id: string) {
    return this.usersService.findOne(id);
  }
}
