import { HttpException, Injectable } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { AuthUserDto } from 'src/users/dto/auth-user.dto';
import { CreateUserDto } from 'src/users/dto/create-user.dto';
import { LoginUserDto } from 'src/users/dto/login-user.dto';
import { UsersService } from 'src/users/users.service';
var bcrypt = require('bcryptjs');

@Injectable()
export class AuthService {
    constructor(private usersService: UsersService, private jwtService: JwtService) {}
    async hashPassword(password: string) : Promise<string> {
        return await bcrypt.hash(password, 10);
    }
    async register(createUserDto: CreateUserDto) : Promise<AuthUserDto> {
        const exisitingUser = await this.usersService.findOneByEmail(createUserDto.email);
        if (exisitingUser)
            throw new HttpException('User already exists', 409);

        const hashedPassword = await this.hashPassword(createUserDto.password);
        const passwordPassed = createUserDto.password;
        createUserDto.password = hashedPassword;
        createUserDto.roles = ['user'];
        const user = await this.usersService.create(createUserDto);
        return this.login({email: createUserDto.email, password: passwordPassed});
    }

    async comparePassword(password: string, hashedPassword: string) : Promise<boolean> {
        return await bcrypt.compare(password, hashedPassword);
    }

    async validateUser(loginUserDto: LoginUserDto) : Promise<AuthUserDto> {
        const user = await this.usersService.findOneByEmail(loginUserDto.email);
        if (!user)
            throw new HttpException('Invalid credentials', 401);
        const isPasswordMatching = await this.comparePassword(loginUserDto.password, user.password);
        if (!isPasswordMatching)
            throw new HttpException('Invalid password', 401);
        const res = this.usersService._mapAuthUserDto(user);
        
        return res;
    }

    async login(loginUserDto: LoginUserDto) : Promise<AuthUserDto> {
        const user = await this.validateUser(loginUserDto);
        const jwt = await this.jwtService.signAsync({ user });
        user.authToken = jwt;
        return user;
    }
}
