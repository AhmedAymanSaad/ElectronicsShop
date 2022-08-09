import { Body, Controller, Post } from '@nestjs/common';
import { AuthUserDto } from 'src/users/dto/auth-user.dto';
import { CreateUserDto } from 'src/users/dto/create-user.dto';
import { LoginUserDto } from 'src/users/dto/login-user.dto';
import { AuthService } from './auth.service';

@Controller('auth')
export class AuthController {
    constructor(private authService: AuthService) { }
    @Post('register')
    async register(@Body() createUserDto: CreateUserDto) {
        return await this.authService.register(createUserDto);
    }
    @Post('login')
    async login(@Body() loginUserDto: LoginUserDto): Promise<AuthUserDto> {
        return await this.authService.login(loginUserDto);
    }
}
