import Slider from "react-slick";
import Experiance from "../Scene/Experiance";
import { Suspense, useState } from "react";
import { getAllProductReviews } from "../Api";
import type { CartProduct, product, Reviews } from "../Types";
import { Await, useLoaderData, useLocation } from "react-router-dom";

// --------------------------------------------------------------------------------
const settings = {
  // arrows: false,
  dots: false,

  fade: true,
  infinite: true,

  slidesToShow: 1,
  slidesToScroll: 1,

  speed: 200,
  autoplaySpeed: 3000,
  // autoplay: true,

  adaptiveHeight: true,
  centerMode: true,

  // className: "f"
};

// --------------------------------------------------------------------------------
export async function loader({ params }: any) {
  const { id } = params;

  return { Reviews: getAllProductReviews(id) };
}

function loadCart() {
  const savedCart = JSON.parse(localStorage.getItem("cart")!);
  return savedCart || [];
}

// --------------------------------------------------------------------------------
export default function Product() {
  const [cart, setCart] = useState(loadCart);

  const allProductReviews = useLoaderData();
  const location = useLocation();

  const data = location.state || {};
  const images: string[] = [];

  // --------------------------------------------------------------------------------
  function addToCart(product: product) {
    // Check if the product already exists in the cart
    const existingProduct = cart.find(
      (item: CartProduct) => item.id === product.id,
    );

    let updatedCart;
    if (existingProduct) {
      // If product exists, update the quantity
      updatedCart = cart.map((item: CartProduct) =>
        item.id === product.id
          ? { ...item, quantity: item.quantity + 1 } // Increase quantity by 1
          : item,
      );
    } else {
      // If product doesn't exist, add it to the cart with quantity 1
      updatedCart = [...cart, { ...product, quantity: 1 }];
    }

    // Update the state and localStorage
    setCart(updatedCart);
    localStorage.setItem("cart", JSON.stringify(updatedCart));
  }

  function getImages(id: number) {
    for (let i = 1; i < 5; i++) {
      images.push(`/src/assets/products_images/rings/00${id}/00${i}.jpg`);
    }
  }

  getImages(data.id);

  function renderAllProductElements(Reviews: any) {
    if (Reviews.length == 0)
      return (
        <div className="Product-Review">
          <p>No reviews for this product</p>
        </div>
      );

    return Reviews.sort((a: Reviews, b: Reviews) =>
      b.createDate.localeCompare(a.createDate),
    ).map((r: Reviews) => {
      const dateObj = new Date(r.createDate);

      return (
        <div key={r.id} className="Product-Review">
          <div className="Product-Review-header">
            <h5>
              By:{" "}
              <i>
                {r.firstName} {r.lastName}
              </i>
            </h5>
            <p>{dateObj.toLocaleDateString()}</p>
          </div>

          <div className="Product-Review-text">
            <p>{r.comment}</p>
            <p>{r.rating}/5</p>
          </div>
        </div>
      );
    });
  }

  // --------------------------------------------------------------------------------
  return (
    <>
      <div className="carousal">
        <Slider {...settings}>
          <Experiance params={data.id} />

          {images.map((section: string, index: number) => (
            <div key={index} style={{ height: "100%" }}>
              <img loading="lazy" className="Product-image" src={section} />
            </div>
          ))}
        </Slider>
      </div>

      <div className="Product-cta-container">
        <div className="Product-Header-Container">
          <h1>{data.name}</h1>
          <p>{data.description}</p>
        </div>

        <div className="Product-Price-Container">
          <p className=" Product-Price">${data.price.toFixed(2)}</p>

          <button onClick={() => addToCart(data)} className="glass button">
            Add to cart
          </button>
        </div>
      </div>

      <div className="Product-Main-Container">
        <div className=" Product-Text-Container">
          {icons.map((icon) => (
            <div key={icon.id} className="glass product-icon-container">
              <p>
                <i className={`product-icon nf ${icon.nf}`} />
              </p>
              <h6>{icon.title}</h6>
              <p>{data[`${icon.title.toLowerCase()}`]}</p>
            </div>
          ))}
        </div>
      </div>

      <div className="Product-Reviews-container">
        <h2>Reviews</h2>

        <Suspense>
          <Await resolve={allProductReviews.Reviews}>
            {renderAllProductElements}
          </Await>
        </Suspense>
      </div>
    </>
  );
}

// --------------------------------------------------------------------------------
const icons = [
  {
    id: 1,
    title: "Carat",
    nf: "nf-md-weight",
  },
  {
    id: 2,
    title: "Material",
    nf: "nf-md-gold",
  },
  {
    id: 3,
    title: "Gemstone",
    nf: "nf-fa-gem",
  },
  {
    id: 4,
    title: "Cut",
    nf: "nf-md-box_cutter",
  },
  {
    id: 5,
    title: "Clarity",
    nf: "nf-cod-eye",
  },
  {
    id: 6,
    title: "Certification",
    nf: "nf-fa-award",
  },
];

// <div className="Product-image-Container card product-card top-card">
