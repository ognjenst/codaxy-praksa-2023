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
        console.log("user:", user);

        //post request for login, returns token, save token to local storage
        //if login is successfull, go to devices route
        //if not, popup
        if (user.username === null || user.password === null) this.createToast("Please provide your username and password.", "error");
        else {
            try {
                let response = await POST("/user/login", user);
                window.localStorage.setItem("auth", response.data.Jwt);
                History.pushState({}, "", "/devices");
            } catch (e) {
                console.log(e);
                History.pushState({}, "", "/devices");
            }
        }
        this.createToast("Your username or password is incorrect. Please try again.", "error");
    }

    async handleEnter(e) {
        if (e.key === "Enter") {
            this.login();
        }
    }
}
