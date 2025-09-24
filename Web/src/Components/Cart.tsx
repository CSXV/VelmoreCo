import { useEffect, useState } from "react";
import type { CartProduct, product } from "./Types";

function loadCart() {
  const savedCart = JSON.parse(localStorage.getItem("cart")!);
  return savedCart || [];
}

export default function Cart() {
  const [total, setTotal] = useState(0);
  const [cart, setCart] = useState(loadCart);

  useEffect(() => {
    // Step 1: Get the products array from localStorage
    const storedProducts = localStorage.getItem("cart");

    if (storedProducts) {
      try {
        const products = JSON.parse(storedProducts);

        // Step 2: Calculate total price
        const totalPrice = products.reduce((sum: number, product: product) => {
          return sum + (parseFloat(product.price) || 0);
        }, 0);

        // Step 3: Update state
        setTotal(totalPrice);
      } catch (error) {
        console.error("Error parsing products from localStorage", error);
      }
    }
  }, []);

  function removeFromCart(productId: number) {
    const updatedCart = cart.filter(
      (item: CartProduct) => item.id !== productId,
    );

    setCart(updatedCart);
    localStorage.setItem("cart", JSON.stringify(updatedCart));
  }

  return (
    <div className="cart-container">
      {cart.length == 0 ? (
        <div>
          <h1>Shopping cart is empty</h1>
          <p>Go to shopping page and add some products</p>
        </div>
      ) : (
        <div>
          <h1>Shopping cart</h1>

          {cart.map((p: CartProduct) => {
            return (
              <div key={p.id} className="a-cart">
                <div className="cart-header">
                  <img
                    width="150px"
                    height="auto"
                    src={`/src/assets/products_images/rings/00${p.id}/001.jpg`}
                  />

                  <h5>{p.name}</h5>
                </div>

                <div className="cart-price">
                  <div>
                    <p>
                      Final price:
                      <br /> <i>${p.price.toFixed(2)}</i>
                    </p>
                    <p>Quantity: {p.quantity}</p>
                  </div>

                  <br />
                  <button
                    onClick={() => removeFromCart(p.id)}
                    className="glass button"
                  >
                    Remove
                  </button>
                </div>
              </div>
            );
          })}

          <div className="a-cart">
            <h3>
              Total: <span>${total.toFixed(2)}</span>
            </h3>
            <button className="glass button">Checkout</button>
          </div>
        </div>
      )}
    </div>
  );
}
