import { LabeledContainer } from "cx/widgets";

export const ContactComponent = () => (
    <cx>
        <LabeledContainer
            label={{
                text: "Contact",
                className: "text-base text-slate-600",
            }}
        >
            <div text-tpl="{$page.device.contact}" className="flex-1 text-slate-600 ml-4" />
        </LabeledContainer>
    </cx>
);