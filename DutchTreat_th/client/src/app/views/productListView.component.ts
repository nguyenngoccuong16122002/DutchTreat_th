import { Component, OnDestroy, OnInit } from "@angular/core";
import { from } from "rxjs";
import { Store } from "../services/store.service";
import { Product } from "../shared/Product"
@Component({
    selector:"product-list",
    templateUrl: "productListView.component.html",
    styleUrls: ['productListView.component.css'],
})
export default class ProductListView implements OnInit {

    products: Product[];
    constructor(public store: Store) {
        this.products = []
    }
 
    ngOnInit(): void {
        this.store.loadProducts().subscribe((data) => {
            this.products = data.map((item) => {
                
                return {
                    id: item.id,
                    category: item.category,
                    size: item.size,
                    price: item.price,
                    title: item.title,
                    artDescription: item.artDescription,
                    artDating: item.artDating,
                    artId: item.artId,
                    artist: item.artist,
                    artistBirthDate: item.artistBirthDate,
                    artistDeathDate: item.artistDeathDate,
                    artistNationality: item.artistNationality
                }
            })
        })
    }
    
}