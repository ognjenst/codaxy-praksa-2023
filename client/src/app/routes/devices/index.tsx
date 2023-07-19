import { createAccessorModelProxy } from "cx/data";
import { FirstVisibleChildLayout, Instance, PureContainer, computable, expr } from "cx/ui";
import { Button, Grid, GridColumnConfig, Icon, Link, Section } from "cx/widgets";
import { ButtonMod } from "../../types/buttonMod";
import { Icons } from "../../types/icons";
import { Status } from "../../types/status";
import Controller from "./Controller";
import { DevicesPageModel } from "./page";

let { $page, $log, $device, $capability } = createAccessorModelProxy<DevicesPageModel>();

const gridColumns = [
    {
        header: "Type",
        align: "center",
        field: "$record.type",
        defaultWidth: 60,
        items: (
            <cx>
                <Icon name="light-bulb" />
            </cx>
        ),
    },
    {
        header: "State",
        field: "$record.state.state",
        align: "center",
        defaultWidth: 60,
        items: (
            <cx>
                <span text={computable("$record.state", (state) => (state ? (state.state ? "On" : "Off") : ""))} />
            </cx>
        ),
    },
    {
        header: "Device name",
        field: "$record.id",
        items: (
            <cx>
                <Link
                    href-tpl="~/devices/{$record.id}"
                    url-bind="url"
                    text-bind="$record.id"
                    className="text-blue-400 hover:text-blue-600"
                />
            </cx>
        ),
    },
    {
        header: "Actions",
        defaultWidth: 80,
        align: "center",
        items: (
            <cx>
                <Button icon="cog" mod="hollow" />
            </cx>
        ),
    },
] as GridColumnConfig[];

export default () => (
    <cx>
        <div controller={Controller}>
            <Section
                header={
                    <cx>
                        <div
                            style={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "center",
                            }}
                        >
                            <h3>Devices</h3>
                            <Button
                                mod={ButtonMod.Primary}
                                icon={expr($page.status.request, (status) => (status === Status.Loading ? Icons.Loading : Icons.Refresh))}
                                onClick={(_, { controller }: Instance<DevicesPageModel, Controller>) => controller.onGetDevices()}
                                disabled={expr($page.status.request, (status) => status === Status.Loading)}
                            />
                        </div>
                    </cx>
                }
            >
                <FirstVisibleChildLayout>
                    <PureContainer if={expr($page.status.request, (status) => status === Status.Loading)}>
                        <Icon name={Icons.Loading} style={{ paddingRight: "0.5rem" }} />
                        <span>Loading...</span>
                    </PureContainer>

                    <Grid records-bind={$page.devices} columns={gridColumns} scrollable />
                </FirstVisibleChildLayout>
            </Section>
        </div>
    </cx>
);
