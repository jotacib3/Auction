import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';



@Component({
    selector: 'app-catalog-list',
    templateUrl: './catalog-list.component.html',
    styleUrls: ['./catalog-list.component.css']
})
export class CatalogListComponent implements OnInit {

    @Input() data: Array<any>;
    @Input() title: string;
    @Output() addItemEvent = new EventEmitter<any>();
    @Output() editItemEvent = new EventEmitter<any>();
    @Output() deleteItemEvent = new EventEmitter<any>();
    value: any;

    edit: boolean;
    add: boolean;

    ngOnInit(): void {
        this.edit = false;
        this.add = false;
    }

    showAdd() {
        this.edit = false;
        this.add = true;
    }
    showEdit(item: any) {
        this.value = item;
        this.add = false;
        this.edit = true;
    }
    onAdd(item: any) {
        this.addItemEvent.emit(item);
        this.add = false;
    }

    onEdit(item: any) {
        this.editItemEvent.emit(item);
        this.edit = false;
    }

    onDelete(item: any) {
        this.deleteItemEvent.emit(item);
        this.edit = false;
        this.add = false;
    }
}
