import { PropertySelection, PureContainer, bind, computable } from "cx/ui";
import Controller from "./Controller";
import { Grid, Heading, Label, List, MsgBox } from "cx/widgets";
import { BrightnessComponent } from "./brightnessComponent";

export default () => (
    <cx>
        |
        <PureContainer controller={Controller}>
            <Heading level="1" text-bind="$page.device.id" />
            <div visible-bind="$page.device.light">
                <BrightnessComponent />
            </div>
        </PureContainer>
    </cx>
);
