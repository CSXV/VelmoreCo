import { Link } from "react-router-dom";

function Home() {
  return (
    <>
      <section className="card hero top-card full-cards">
        <h1>Discover the Art of Timeless Luxury</h1>
        <p className="home-paragraph">
          Crafted with unparalleled precision, our jewelry celebrates beauty,
          elegance, and exclusivity.
        </p>

        <Link className="button glass" to="shop">
          Explore Our Collection
        </Link>
      </section>

      <section className="card full-cards">
        <h2>Where Tradition Meets Innovation</h2>
        <p className="home-paragraph">
          For over 50 years, we have been dedicated to creating breathtaking
          jewelry that blends classic elegance with modern artistry. Our pieces
          are designed not just to be worn, but to be treasured. Meticulously
          handcrafted by master jewelers, each piece carries with it a legacy of
          unparalleled craftsmanship and a commitment to excellence. We believe
          that every piece of jewelry should tell a story—yours.
        </p>

        <Link className="glass button" to="about">
          Learn More About Us
        </Link>
      </section>

      <section className="card black-card home-shop-section full-cards">
        <h2>Exclusive Collections, Curated for You</h2>
        <p className="home-paragraph">
          Step into a world where beauty knows no bounds. Our exclusive
          collections are designed to embody the highest level of artistry,
          using the finest gemstones and metals. Whether you are seeking an
          engagement ring that will last a lifetime, or a statement necklace
          that will turn heads at your next gala, our curated pieces are
          designed to suit every occasion and every style.
        </p>

        <Link className="glass button" to="jewelry">
          Shop the Collection
        </Link>
      </section>

      <section className="card full-cards">
        <h2>Your Personal Jewelry Concierge</h2>
        <p className="home-paragraph">
          Our dedicated concierge service is here to help you find the perfect
          piece or answer any questions you may have about our jewelry. Whether
          you need assistance with selecting the right gift or a personal
          consultation for a custom design, we are here to ensure your
          experience is nothing short of exceptional.
        </p>

        <Link className="glass button" to="contact">
          Contact Our Concierge
        </Link>
      </section>
    </>
  );
}

export default Home;
