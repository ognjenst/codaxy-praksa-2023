import Controller from "./Controller";
import { Button, Section, TextField, Link, ValidationGroup } from "cx/widgets";
import BackgroundImage from "../../../assets/img/background2.jpg";

export default () => (
    <cx>
        <div
            className="flex flex-col bg-gray-100 h-screen items-center justify-center"
            controller={Controller}
            style={{
                backgroundImage: `url(${BackgroundImage})`,
                backgroundSize: "180%",
                backgroundPosition: "center",
                backgroundRepeat: "no-repeat",
            }}
        >
            <Section className="rounded-2xl p-4 user-section-background">
                <div className="flex flex-col items-center space-y-6">
                    <div className="text-center font-sans text-gray-500 text-2xl mb-5" text="CREATE NEW ACCOUNT" />
                    <div className="flex flex-col gap-4 flex-1">
                        <ValidationGroup invalid-bind="$page.registration.invalid" tabOnEnterKey>
                            <TextField className="h-11 !w-[250px]" placeholder="First name" value-bind="$page.user.firstName" required />
                            <TextField className="h-11 !w-[250px]" placeholder="Last name" value-bind="$page.user.lastName" required />
                            <TextField
                                className="h-11 !w-[250px]"
                                placeholder="Email"
                                value-bind="$page.user.email"
                                icon="envelope"
                                required
                            />
                            <TextField
                                className="h-11 !w-[250px]"
                                placeholder="Username"
                                value-bind="$page.user.username"
                                icon="user-circle"
                                required
                            />
                            <TextField
                                className="h-11 !w-[250px]"
                                placeholder="Password"
                                inputType="password"
                                value-bind="$page.user.password"
                                icon="key"
                                required
                                onKeyDown="handleEnter"
                            />
                        </ValidationGroup>
                        <Button className="text-xl h-12 text-gray-100 !bg-[#4cbdf9]" onClick="registration" text="SIGN UP" />
                    </div>
                    <Link
                        onClick={(e, { store }) => {
                            store.set("login", true);
                        }}
                        className="font-sans text-gray-500 text-l bg-transparent underline hover:text-gray-700"
                        text="Already have an account? Sign in here."
                    />
                </div>
            </Section>
        </div>
    </cx>
);
