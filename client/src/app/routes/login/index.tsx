import Controller from "./Controller";
import { Button, Section, TextField, Link, ValidationGroup } from "cx/widgets";
import BackgroundImage from "../../../assets/img/background2.jpg";

export default () => (
    <cx>
        <div
            className="flex flex-col bg-gray-100 h-screen items-center justify-center"
            style={{
                backgroundImage: `url(${BackgroundImage})`,
                backgroundSize: "180%",
                backgroundPosition: "center",
                backgroundRepeat: "no-repeat",
            }}
            controller={Controller}
        >
            <Section className="rounded-2xl p-4 user-section-background">
                <div className="flex flex-col items-center space-y-6">
                    <div className="text-center font-sans text-gray-500 text-2xl" text="WELCOME" />
                    <div className="flex flex-col gap-4 flex-1">
                        <ValidationGroup invalid-bind="$page.login.invalid">
                            <TextField
                                className="h-11 !w-[250px]"
                                placeholder="Username"
                                value-bind="$page.user.username"
                                icon="user-circle"
                                onKeyDown="handleEnter"
                                required
                            />
                            <TextField
                                className="h-11 !w-[250px]"
                                placeholder="Password"
                                inputType="password"
                                value-bind="$page.user.password"
                                icon="key"
                                onKeyDown="handleEnter"
                                required
                            />
                        </ValidationGroup>
                        <Button className="text-xl h-12 text-gray-100 !bg-[#4cbdf9]" onClick="login" text="LOGIN" />
                    </div>
                    <Link
                        onClick={(e, { store }) => {
                            store.set("login", false);
                        }}
                        className="font-sans text-gray-500 text-sm bg-transparent underline hover:text-gray-700 flex-1"
                        text=" Don't have an account yet? Sign up here."
                    />
                </div>
            </Section>
        </div>
    </cx>
);
