import { Controller } from "cx/ui";
import { GET } from '../../../api/util/methods';

export default class extends Controller {
    onInit(): void {
        this.loadData();
     }
  
     async loadData() {
        let id = this.store.get('$route.id');
       try{
         let resp = await GET(`/devices/${id}`);
         this.store.set('$page.device', resp);
         console.log(this.store.get('$page.device'));
       } catch(err){
         console.error(err);
       }
     }
}