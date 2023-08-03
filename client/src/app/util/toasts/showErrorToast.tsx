import { Toast } from "cx/widgets";

export async function showErrorToast(message, mod) {
    let toast = Toast.create({
        message: message,
        placement: "top",
        mod: mod,
        timeout: 4000,
    });
    toast.open();
}
