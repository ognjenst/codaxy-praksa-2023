import { ContentResolver, FirstVisibleChildLayout } from "cx/ui";
import { DocumentTitle, PureContainer, RedirectRoute, Route } from "cx/widgets";
import About from "./about";
import Widgets from "./widgets";
import Pages from "./pages";
import Dashboard from "./dashboard";
import { CheckerLayout } from "../layout/CheckerLayout";
import SignIn from "./pages/sign-in";
import { SandboxedRoute } from "../components/SandboxedRoute";
import InvoiceRoutes from "./invoices";
import { PageNotImplemented } from "../components/PageNotImplemented";
import Devices from "./devices";
import DeviceDetails from "./devices/device-details";
import Automations from "./automations";

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
        <DocumentTitle append text="Demo App" separator=" | " />
    </cx>
);
