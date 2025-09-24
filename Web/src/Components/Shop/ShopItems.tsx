import { Await, Link } from "react-router-dom";
import { Suspense } from "react";
import type { product } from "../Types";

export default function ShopItems({ params: allProductsData }: any) {
  function renderAllProducts(data: product[]) {
    return data.map((i) => (
      <article key={i.id} className="item-container">
        <div className="shop-image-conatiner">
          <img
            className="shop-item-image"
            src={`/src/assets/products_images/rings/00${i.id}/001.jpg`}
          />
        </div>

        <div className="shop-item-info-container">
          <h4 className="shop-item-header">{i.name}</h4>
          <p className="shop-item-price">${i.price}</p>
        </div>

        <Link
          className="button glass shop-item-button"
          to={`${i.id}`}
          state={i}
        >
          More info
        </Link>
      </article>
    ));
  }

  return (
    <section className="shop-items-container" id="collection">
      <Suspense>
        <Await resolve={allProductsData.Products}>{renderAllProducts}</Await>
      </Suspense>
    </section>
  );
}
