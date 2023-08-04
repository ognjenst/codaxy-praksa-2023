import { ContentResolver, FirstVisibleChildLayout, PureContainer, computable } from "cx/ui";
import { DocumentTitle, RedirectRoute, Route } from "cx/widgets";
import { SandboxedRoute } from "../components/SandboxedRoute";
import { CheckerLayout } from "../layout/CheckerLayout";
import Workflows from "../routes/workflows";
import Triggers from "./triggers";
import Automations from "./automations";
import Dashboard from "./dashboard";
import Devices from "./devices";
import DeviceDetails from "./devices/device-details";
import Login from "./login";
import Registration from "./registration";

export default () => (
    <cx>
        <FirstVisibleChildLayout>
            <RedirectRoute route="~/" redirect="~/login" url-bind="url" />

            <Login
                visible={(store) => {
                    return !localStorage.getItem("auth") && store.login;
                }}
            />
            <Registration
                visible={(store) => {
                    return !localStorage.getItem("auth") && !store.login;
                }}
            />

            <CheckerLayout>
                <SandboxedRoute route="~/dashboard">
                    <Dashboard />
                </SandboxedRoute>
                <Route route="~/devices" url-bind="url">
                    <Devices />
                </Route>
                <Route route="~/workflows" url-bind="url">
                    <Workflows />
                </Route>
                <Route route="~/triggers" url-bind="url">
                    <Triggers />
                </Route>
                <Route route="~/automations" url-bind="url">
                    <Automations />
                </Route>
                <SandboxedRoute route="~/devices/:id">
                    <DeviceDetails />
                </SandboxedRoute>
            </CheckerLayout>
        </FirstVisibleChildLayout>

        <ContentResolver
            visible-expr="!!{user}"
            params={1}
            onResolve={() => import(/* webpackChunkName: "user-routes" */ "./user").then((x) => x.default)}
        />
        <ContentResolver params={1} onResolve={() => import(/* webpackChunkName: "overlays" */ "../overlays").then((x) => x.default)} />
        <DocumentTitle append text="SOC" separator=" | " />
    </cx>
);
