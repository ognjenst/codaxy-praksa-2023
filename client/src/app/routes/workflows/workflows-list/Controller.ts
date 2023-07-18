import { Controller } from 'cx/ui';
import { GET } from '../../../api/util/methods';

export default class extends Controller {
    onInit(): void {
        let workflows = [
            {
              "name": "Morning routine",
              "version": 0,
              "enabled": false,
              "createdAt": "0001-01-01T00:00:00",
              "updatedAt": "0001-01-01T00:00:00"
            },
            {
              "name": "Mail received",
              "version": 0,
              "enabled": false,
              "createdAt": "0001-01-01T00:00:00",
              "updatedAt": "0001-01-01T00:00:00"
            },
            {
              "name": "Locked lab mode",
              "version": 0,
              "enabled": false,
              "createdAt": "0001-01-01T00:00:00",
              "updatedAt": "0001-01-01T00:00:00"
            }
          ];
        
        this.store.set('$page.workflows', workflows);
    }

    async loadData() {
        console.log('load data');
        try {
            let resp = await GET('/workflows');
            this.store.set('$page.workflows', resp);
        } catch (err) {
            console.log(err);
        }
    }  
}