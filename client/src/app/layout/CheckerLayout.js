import { computable, History } from "cx/ui";
import { Dropdown, HighlightedSearchText, Icon, Link, List, Menu, MenuItem, PureContainer, Submenu, TextField } from "cx/widgets";
import { Logo2 } from "../components/Logo2";
import Controller from "./Controller";

const NavItem = ({ text, href, tooltip, onClick, className, icon, badge, expanded }) => (
    <cx>
        <Link
            href={href}
            url-bind="url"
            class="hover:bg-gray-100 flex items-center px-3 py-3 text-gray-800 relative font-semibold whitespace-nowrap text-opacity-70 text-[15px] border-l-[3px] border-transparent cursor-pointer"
            className={className}
            activeClass="!bg-blue-100 !border-blue-500 !text-blue-500 !opacity-100"
            tooltip={tooltip}
            onClick={onClick}
            match="subroute"
        >
            <Icon name={icon} class="w-7 h-7 ml-3 mr-3 opacity-70" />
            <div text={text} class="flex-grow" />
            <div text={badge} visible={badge} class="text-xs bg-black bg-opacity-20 rounded-full px-3 py-1" />
            <Icon
                name="drop-down"
                class="w-5 h-5 mr-2 transform transition-all opacity-80"
                visible={!!expanded}
                className={{
                    "rotate-180": expanded,
                }}
            />
        </Link>
    </cx>
);

const GroupItem = ({ text, href, tooltip, className, icon, badge, children, expanded }) => (
    <cx>
        <NavItem
            href={href}
            text={text}
            tooltip={tooltip}
            className={className}
            icon={icon}
            badge={badge}
            onClick={(e, { store }) => {
                e.preventDefault();
                store.toggle(expanded.bind);
            }}
            expanded={expanded}
        />
        <PureContainer visible={expanded}>{children}</PureContainer>
    </cx>
);

const ChildItem = ({ text, href, badge }) => (
    <cx>
        <NavItem href={href} text={text} className="!pl-16 opacity-80" badge={badge} />
    </cx>
);

export const CheckerLayout = ({ children, nav }) => (
    <cx>
        <div
            class="h-full grid grid-cols-2 grid-rows-2"
            style="grid-template-columns: 250px 1fr; grid-template-rows: auto 1fr"
            controller={Controller}
        >
            <div class="border-r border-b py-2 pl-6 flex">
                <Logo2 />
            </div>
            <div class="border-b flex justify-end">
                <div
                    class="border-l"
                    onClick={(e, { store }) => {
                        store.toggle("nav.expand.user");
                    }}
                    tabIndex="0"
                    className=" float-right"
                >
                    <div class="flex items-center px-4 py-2 cursor-pointer">
                        <div class="w-10 h-10 bg-gray-300 rounded-full align-middle flex items-center justify-center relative flex-shrink-0 cursor-pointer">
                            <span text-bind="user.initials" visible-expr="!{user.pictureUrl}" />
                            <img
                                src-tpl="{user.pictureUrl}"
                                visible-expr="!!{user.pictureUrl}"
                                class="w-full h-full object-cover rounded-full absolute left-0 top-0"
                            />
                        </div>
                        <div class="ml-4 mr-4 leading-tight">
                            <div text-tpl="{user.firstName} {user.lastName}">Test</div>
                            <div class="opacity-50 text-sm" text-bind="user.email" />
                        </div>
                        <Icon
                            name="drop-down"
                            class="w-4 h-4 transform transition-all opacity-50"
                            className={{
                                "rotate-180": { bind: "nav.expand.user" },
                            }}
                        />
                    </div>
                    <Dropdown
                        visible-bind="nav.expand.user"
                        dismissOnFocusOut
                        arrow
                        offset={5}
                        focusable
                        autoFocus
                        placementOrder={"down-left"}
                    >
                        <Menu class="m-2">
                            <MenuItem onClick="onSignOut">Sign Out</MenuItem>
                        </Menu>
                    </Dropdown>
                </div>
            </div>
            <div class="border-r pt-3">
                <div class="px-6 py-3 text-gray-400 text-sm">Main Menu</div>

                <NavItem text="Devices" icon="light-bulb" href="~/devices" />
                <NavItem text="Automations" icon="wrench-screwdriver" href="~/automations" />
                <NavItem text="Workflows" icon="arrow-down-circle" href="~/workflows" />
                <NavItem text="Triggers" icon="bolt" href="~/triggers" />
            </div>
            {children}
        </div>
    </cx>
);
