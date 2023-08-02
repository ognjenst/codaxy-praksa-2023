import { Controller } from "cx/ui";
import { Toast } from "cx/widgets";
import { POST } from "../../api/util/methods";
import { History } from "cx/ui";

export default class extends Controller {
    onInit(): void {}

    async createToast(message, mod) {
        let toast = Toast.create({
            message: message,
            placement: "top",
            mod: mod,
            timeout: 4000,
        });
        toast.open();
    }

    async registration() {
        let newUser = this.store.get("$page.user");
        console.log("user:", newUser);

        if (
            newUser.firstname === null ||
            newUser.lastname === null ||
            newUser.email === null ||
            newUser.username === null ||
            newUser.password === null
        )
            this.createToast("Please provide the required fields.", "error");
        else {
            await POST("/user/registration", newUser)
                .then(() => {
                    History.pushState({}, "", "/devices");
                })
                .catch((e) => {
                    History.pushState({}, "", "/devices");
                    console.log(e);
                });
        }

        //if all fields are valid, post request for registration
        //go to devices route
    }

    async handleEnter(e) {
        if (e.key === "Enter") {
            this.registration();
        }
    }
}
