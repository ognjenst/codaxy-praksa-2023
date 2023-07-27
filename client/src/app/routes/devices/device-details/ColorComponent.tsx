import { Instance, Repeater, bind, computable } from "cx/ui";
import { Button, LabeledContainer } from "cx/widgets";
import Controller from "./Controller";

export const ColorComponent = () => (
    <cx>
        <LabeledContainer
            label={{
                text: "Color",
                className: "text-base text-slate-600",
            }}
        >
            <Repeater records={computable("$page.colors", (colors) => Object.keys(colors))}>
                <Button
                    style={{
                        backgroundColor: computable("$record", "$page.device.state.state", (record, state) => (state ? record : "#ABABAB")),
                    }}
                    onClick="changeColors"
                    enabled-bind="$page.device.state.state"
                />
            </Repeater>
        </LabeledContainer>
    </cx>
);
