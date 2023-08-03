import { Controller } from "cx/ui";
import { Toast } from "cx/widgets";
import { POST } from "../../api/util/methods";
import { History } from "cx/ui";
import { showErrorToast } from "../../util/toasts";

export default class extends Controller {
    onInit(): void {}

    async registration() {
        let newUser = this.store.get("$page.user");
        console.log("user:", newUser);

        const invalid = this.store.get("$page.registration.invalid");

        if (invalid) return showErrorToast("Please provide your username and password.", "error");

        await POST("/user/register", newUser)
            .then(() => {
                History.pushState({}, null, "/devices");
            })
            .catch((e) => {
                showErrorToast("Registration unsuccessfull.", "error");
                console.log(e);
            });
    }

    async handleEnter(e) {
        if (e.key === "Enter") {
            this.registration();
        }
    }
}
