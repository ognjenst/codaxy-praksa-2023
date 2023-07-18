import { Controller } from "cx/ui";
import { GET } from "../../api/util/methods";

export default class extends Controller{
    onInit(){
        //this.store.set("$page.trigger", "triger");
        this.loadData();
    }

    async loadData(){
        try{
        let resp = await GET('/workflows');
        this.store.set('$page.workflows', resp);
        }catch(err){
            console.log(err);
        }

    }

    addAutomation(){

    }

    addTrigger(){

    }
}