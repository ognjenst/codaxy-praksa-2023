import { Heading } from "cx/widgets";
import Controller from "./Controller";
import { BrightnessComponent } from "./BrightnessComponent";

export default () => (
    <cx>
        <div controller={Controller}>
            <Heading level="1" text-bind="$page.device.id" />
            <div visible-bind="$page.device.light">
                <BrightnessComponent />
            </div>
        </div>
    </cx>
);
