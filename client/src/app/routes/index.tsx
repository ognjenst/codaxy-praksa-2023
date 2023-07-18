import { ContentResolver, FirstVisibleChildLayout } from "cx/ui";
import { DocumentTitle, RedirectRoute, Route } from "cx/widgets";
import { SandboxedRoute } from "../components/SandboxedRoute";
import { CheckerLayout } from "../layout/CheckerLayout";
import Automations from "./automations";
import Dashboard from "./dashboard";
import Devices from "./devices";
import DeviceDetails from "./devices/device-details";

export default () => (
    <cx>
        <FirstVisibleChildLayout>
            <RedirectRoute route="~/" redirect="~/devices" url-bind="url" />

            <CheckerLayout>
                <SandboxedRoute route="~/dashboard">
                    <Dashboard />
                </SandboxedRoute>

                <Route route="~/devices" url-bind="url">
                    <Devices />
                </Route>
                <SandboxedRoute route="~/devices/:id">
                    <DeviceDetails />
                </SandboxedRoute>
                <Route route="~/automations" url-bind="url">
                    <Automations />
                </Route>
            </CheckerLayout>
        </FirstVisibleChildLayout>

        <ContentResolver
            visible-expr="!!{user}"
            params={1}
            onResolve={() => import(/* webpackChunkName: "user-routes" */ "./user").then((x) => x.default)}
        />
        <ContentResolver params={1} onResolve={() => import(/* webpackChunkName: "overlays" */ "../overlays").then((x) => x.default)} />
        <DocumentTitle append text="Demo App" separator=" | " />
    </cx>
);
