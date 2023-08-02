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

    async login() {
        let user = this.store.get("$page.user");

        if (user.username === null || user.password === null) this.createToast("Please provide your username and password.", "error");
        else {
            try {
                let response = await POST("/user/login", user);

                this.store.set("authUser", user);
                window.localStorage.setItem("auth", response.jwt);
                History.pushState({}, "", "/devices");
            } catch (e) {
                console.log(e);
                this.createToast("Your username or password is incorrect. Please try again.", "error");
            }
        }
    }

    async handleEnter(e) {
        if (e.key === "Enter") {
            this.login();
        }
    }
}
