export class OrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    productId: number;
    productCategory: string;
    productSize: string;
    productTitle: string;
    productArtist: string;
    productArtId: string;
    constructor() {
        this.id = 0;
        this.quantity = 0;
        this.unitPrice = 0;
        this.productId = 0;
        this.productCategory = "";
        this.productSize = "";
        this.productTitle = "";
        this.productArtist = "";
        this.productArtId = "";
       
    }
}


export class Order {
    orderId: number = 0;
    orderDate: Date = new Date();
    orderNumber: string = Math.random().toString(36).substring(1,5);
    items: OrderItem[] = [];

    get subtotal(): number {

        const result = this.items.reduce(
            (tot, val) => {
                return tot + (val.unitPrice * val.quantity);
            }, 0);

        return result;
    }
   
}