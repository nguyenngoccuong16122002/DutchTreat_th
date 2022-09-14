import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { from } from "rxjs";
import { map } from "rxjs/operators";
import { Product } from "../shared/Product"
import { Order, OrderItem } from "../shared/Order"
import { LoginRequest, LoginResults } from "../shared/LoginResults";
@Injectable()



export class Store {

    constructor(private http: HttpClient) {

    }
    public order: Order = new Order();
    public token = "";
    public expiration = new Date();
    loadProducts() {
        return this.http.get<Product[]>("/api/products");
    }

    addToOrder(product: Product) {

        let item = this.order.items.find(o => o.productId === product.id);

        if (item) {
            item.quantity++;
        } else {
            item = new OrderItem();
            item.productId = product.id;
            item.productTitle = product.title;
            item.productArtId = product.artId;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.quantity = 1;

            this.order.items.push(item);
        }

    
    }


    get loginRequired(): boolean {
        return this.token.length === 0 || this.expiration > new Date();
    }

    login(creds: LoginRequest) {
        return this.http.post<LoginResults>("/account/createtoken", creds)
            .pipe(map(data => {
                this.token = data.token;
                this.expiration = data.expiration;
            }));
    }

    
    checkout() {
        const headers = new HttpHeaders().set("Authorization", `Bearer ${this.token}`);
        debugger;
        console.log(this.order);

        return this.http.post("/api/orders", this.order, {
            headers: headers
        }).pipe(map(() => {
                this.order = new Order();
            }));
    }
}

//export class Product {
//    id: number;
//    title: string;
//    price: number;
//    constructor() {
//        this.title = "";
//        this.id = 0;
//        this.price = 0;
//    }
//}
//export class Product {
//    id: number;
//    category: string;
//    size: string;
//    price: number;
//    title: string;
//    artDescription: string;
//    artDating: string;
//    artId: string;
//    artist: string;
//    artistBirthDate: Date;
//    artistDeathDate: Date;
//    artistNationality: string;
//    constructor() {
//        this.id = 0;
//        this.category = "";
//        this.size = "";
//        this.price = 0;
//        this.title = "";
//        this.artDescription = "";
//        this.artDating = "";
//        this.artId = "";
//        this.artist = "";
//        this.artistBirthDate = new Date;
//        this.artistDeathDate = new Date;
//        this.artistNationality = "";
//    }
//}