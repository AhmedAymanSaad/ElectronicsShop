import { Prop, Schema, SchemaFactory } from "@nestjs/mongoose";
import { timeStamp } from "console";
import mongoose, { Document } from "mongoose";

@Schema(
    {
        validateBeforeSave: true,
        timestamps: true
    }
)
export class Order extends Document {
    
    @Prop({type: mongoose.Schema.Types.ObjectId, ref: "Product"})
    productId

    @Prop({type: mongoose.Schema.Types.ObjectId, ref: "User"})
    userId
    

    @Prop({required: true})
    quantity: number;

    @Prop({required: true})
    price: number;

    @Prop({ enum: ["pending", "delivered", "cancelled"], default: "pending"})
    status: string;

    

    @Prop({type: Date , default: Date.now})
    createdAt: Date;
    

}

export const OrderSchema = SchemaFactory.createForClass(Order);
