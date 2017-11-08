import { Component, Input, OnInit } from '@angular/core';
import { asEnumerable } from 'linq-es2015';
import { RTOption } from '../../models/rt-option';


@Component({
    selector: 'responsive-table',
    templateUrl: './responsive-table.component.html',
    styleUrls: ['./responsive-table.component.css']
})
export class ResponsiveTableComponent implements OnInit
{
    @Input('rtoption') option: RTOption;

    private isSortAscending: boolean = true;

    pageSize = 10;
    itemsList: any[] = [];
    currentPage = 1;
    pages: number[] = [];
    searchParam: string;

    constructor() { }

    ngOnInit()
    {
        this.option.columns.unshift('');
        this.itemsList = this.paginate(this.option.items);
    }

    private paginate(arr: any[])
    {
        return asEnumerable(arr)
            .Select((item, index) =>
            {
                let num = index! < 1 ? 1 : index;
                return <any>{ page: Math.ceil(num! / this.pageSize), item: item };
            })
            .GroupBy(item => item.page)
            .ToArray();

    }

    get totalItems()
    {
        let total = 0;
        this.pages = [];
        for (let i = 0; i < this.itemsList.length; i++)
        {
            total += this.itemsList[i].length;
            this.pages.push(i + 1);
        }

        return total;
    }

    onSearch()
    {
        let foundList = this.searchParam
            ? asEnumerable(this.option.items)
                .Where(item =>
                {
                    let param = this.searchParam.toLowerCase();
                    let name = (<string>item.name).toLowerCase();

                    return name.includes(param);
                })
                .ToArray()
            : this.option.items;

        this.itemsList = this.paginate(foundList);
    }

    sortBy(column: string)
    {
        let list = this.isSortAscending
            ? asEnumerable(this.option.items).OrderBy(i => i[column]).ToArray()
            : asEnumerable(this.option.items).OrderByDescending(i => i[column]).ToArray();

        this.itemsList = this.paginate(list);
        this.isSortAscending = !this.isSortAscending;
    }

    previous()
    {
        if (this.currentPage == 1) return;
        this.currentPage--;
    }

    next()
    {
        if (this.currentPage == this.pages.length) return;
        this.currentPage++;
    }

    changePage(page: number)
    {
        this.currentPage = page;
    }
}
