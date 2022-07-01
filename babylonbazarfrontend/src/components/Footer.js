import Button from "./Button"
import Info from "./Info"

const Footer = () => {
    return (
        <div className="Footer">
            <Info class="Info" text="Copyright OOPanik @2022" />
            <Button buttonStyle="NavBarButtonStyle" buttonTextStyle="NavBarButtonText" link="" text="About" />
            <Button buttonStyle="NavBarButtonStyle" buttonTextStyle="NavBarButtonText" link="" text="Privacy" />
            <Button buttonStyle="NavBarButtonStyle" buttonTextStyle="NavBarButtonText" link="" text="Contact" />
        </div>
    );
}

export default Footer;