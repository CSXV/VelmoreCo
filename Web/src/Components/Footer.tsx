import { Link } from "react-router-dom";

function Footer() {
  return (
    <footer>
      <div className="footer-section">
        <Link to="about">About Us</Link>

        <Link to="shop">Shop All Collections</Link>

        <Link to="contact">Contact Us</Link>
      </div>

      <div className="footer-section">
        <a href="https://facebook.com">
          <i className="nf nf-dev-facebook" />
        </a>
        <a href="https://instagram.com">
          <i className="nf nf-fa-instagram" />
        </a>
        <a href="https://twitter.com">
          <i className="nf nf-dev-twitter" />
        </a>
        <a href="https://linkedin.com">
          <i className="nf nf-dev-linkedin" />
        </a>
      </div>

      <div className="footer-section">
        <p>&copy; 2025 Velmoré & Co. All Rights Reserved.</p>
      </div>
    </footer>
  );
}

export default Footer;
