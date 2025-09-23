import { Link } from "react-router-dom";

export default function Contact() {
  return (
    <>
      <section className="card top-card full-cards">
        <h1 className="fancy-font">
          Your Personal Concierge to the World of Velmoré
        </h1>

        <div>
          <p>
            We’re At Your Service Whether you are seeking a bespoke creation,
            inquiring about a private viewing, or simply wish to speak with one
            of our specialists, our dedicated Client Concierge is here to assist
            with grace, discretion, and care.
          </p>

          <p>
            We invite you to connect with us through the method that best suits
            your preferences. Every inquiry is treated with the utmost
            confidentiality and attention.
          </p>
        </div>
      </section>

      <div className="card full-cards">
        <h2>Contact Options</h2>

        <br />

        <div>
          <div>
            <h3>General Inquiries</h3>
            <p>
              For all questions regarding our collections, services, or your
              recent order:
            </p>
            <p>concierge@VelmoreCo.com</p>
            <p>+1 (000) 000-0000</p>
            <p>Monday – Friday | 10AM – 6PM (GMT)</p>
          </div>

          <br />

          <div>
            <h3>Private Consultation</h3>
            <p>
              Book an appointment with one of our jewelry specialists for a
              personalized experience.
            </p>
            <p>Available in-person or virtually</p>

            <Link className="glass button" to="">
              Schedule a Private Appointment →
            </Link>
          </div>
        </div>
      </div>

      <div className="card full-cards">
        <h2>Boutique Locations</h2>
        <p>By Appointment Only</p>
        <p>Paris • London • New York • Dubai • Tokyo</p>

        <Link className="glass button" to="">
          Find a Boutique →
        </Link>
      </div>

      <div className="card full-cards">
        <h2>Media & Press</h2>
        <p>For media inquiries, editorial requests, and interviews:</p>
        <p>press@VelmoreCo.com</p>
      </div>

      <div className="card full-cards">
        <h2>Connect with Us Privately</h2>
        <p>
          We understand the value of discretion. Your privacy is our priority,
          and all communications are handled with care. For high-value or
          sensitive inquiries, please request a confidential line or secure
          communication channel.
        </p>

        <Link className="glass button" to="">
          Request Confidential Contact →
        </Link>
      </div>
    </>
  );
}
