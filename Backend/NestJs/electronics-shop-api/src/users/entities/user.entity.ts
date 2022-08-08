
import { Prop, Schema, SchemaFactory } from "@nestjs/mongoose";
import { Document } from "mongoose";

@Schema(
    {
        validateBeforeSave: true,
        timestamps: true
    }
)
export class User extends Document {
    @Prop({required: true, unique: true})
    email: string;
    
    @Prop({required: true, minlength: 8})
    password: string;

    @Prop([String])
    roles: string[];

}

export const UserSchema = SchemaFactory.createForClass(User);

/*
import * as mongoose from "mongoose"
export const UserSchema = new mongoose.Schema(
    {
        username: {
            type: String,
            required: true,
            unique: true,
        },
        password: {
            type: String,
            required: true,
            minlength: 8,
        },
    },
    { timestamps: true }
)

export interface User extends mongoose.Document {
    _id: string;
    username: string;
    password: string;
}
*/