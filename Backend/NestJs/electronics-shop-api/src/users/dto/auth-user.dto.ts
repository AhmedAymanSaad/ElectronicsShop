import { IsString, IsNotEmpty, IsEmail, MinLength } from "class-validator";

export class AuthUserDto {
    @IsString()
    @IsNotEmpty()
    @IsEmail()
    userId: string;
    @IsString()
    @IsNotEmpty()
    authToken: string;
    @IsNotEmpty()
    roles: string[];
}