import { Controller } from "cx/ui";
import { Toast } from "cx/widgets";
import { POST } from "../../api/util/methods";
import { History } from "cx/ui";
import { showErrorToast } from "../../util/toasts/showErrorToast";

export default class extends Controller {
    onInit(): void {}

    async login() {
        let user = this.store.get("$page.user");
        const invalid = this.store.get("$page.login.invalid");
        if (invalid) return showErrorToast("Please provide your username and password.", "error");

        try {
            let response = await POST("/user/login", user);

            window.localStorage.setItem("auth", response.jwt);
            History.pushState({}, null, "/devices");
        } catch (e) {
            console.log(e);
            showErrorToast("Your username or password is incorrect. Please try again.", "error");
        }
    }

    async handleEnter(e) {
        if (e.key === "Enter") {
            this.login();
        }
    }
}
