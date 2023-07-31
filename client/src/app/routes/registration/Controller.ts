import { Controller } from "cx/ui";
import { Toast } from "cx/widgets";

export default class extends Controller {
    onInit(): void {}

    async createToast(message, mod) {
        let toast = Toast.create({
            message: message,
            placement: 'top',
            mod: mod,
            timeout: 4000
        });
        toast.open();
    }

    async registration() {
        let newUser = this.store.get("$page.user");
        console.log("user:", newUser);

        if (newUser.firstname === null || newUser.lastname === null || newUser.email === null || newUser.username === null || newUser.password === null)
            this.createToast('Please provide the required fields.', 'error');

        //if all fields are valid, post request for registration
        //go to devices route
    }
}
