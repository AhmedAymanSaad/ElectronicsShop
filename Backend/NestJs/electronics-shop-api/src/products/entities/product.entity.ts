import { Prop, Schema, SchemaFactory } from "@nestjs/mongoose";
import { Document } from "mongoose";

@Schema(
    {
        validateBeforeSave: true,
        timestamps: true
    }
)
export class Product extends Document {
    @Prop({required: true})
    name: string;
    
    @Prop({required: true})
    price: number;

    @Prop({required: true})
    description: string;

    @Prop({required: true})
    image: string;

}

export const ProductSchema = SchemaFactory.createForClass(Product);