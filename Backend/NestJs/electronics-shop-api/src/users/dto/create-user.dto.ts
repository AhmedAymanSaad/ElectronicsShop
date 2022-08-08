import { Exclude } from "class-transformer";
import { IsString, IsNotEmpty, IsEmail, MinLength } from "class-validator";

export class CreateUserDto {
    @IsString()
    @IsNotEmpty()
    @IsEmail()
    email: string;
    
    @IsString()
    @IsNotEmpty()
    @MinLength(8)
    password: string;

    @Exclude()
    roles: string[];
}
