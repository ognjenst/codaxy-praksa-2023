import { Controller } from 'cx/ui';
import { GET } from '../../../api/util/methods';



export default class extends Controller {
    onInit(): void {
        var arr = [
            {"name": "Task 1", "showDescription": false},
            {"name": "Task 2", "showDescription": false},
            {"name": "Task 3", "showDescription": false},
            {"name": "Task 4", "showDescription": false},
            {"name": "Task 5", "showDescription": false},
            {"name": "Task 6", "showDescription": false},
        ]

        this.store.set("$page.someList", arr);
    }
};