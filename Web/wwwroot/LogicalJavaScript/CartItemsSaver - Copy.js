
function addToCart(customerId, productId) {
    const storageData = localStorage.getItem(customerId);

    localKey = customerId
    if (storageData) {

        const customerData = JSON.parse(storageData);


        const dataArray = Array.isArray(customerData) ? customerData : [customerData];


        const existingProduct = dataArray.find(item => item.productId === productId);

        if (existingProduct) {

            existingProduct.quantity += 1;
        } else {

            dataArray.push({ productId, quantity: 1 });
        }


        localStorage.setItem(customerId, JSON.stringify(dataArray));
    } else {

        const newData = [{ productId, quantity: 1 }];



        localStorage.setItem(customerId, JSON.stringify(newData));
    }
    console.log(localStorage.getItem(customerId))

    try {
        displayCustomerProducts(customerId)
    }
    catch (e) {
        console.log("No Element");
    }

}


function clearCart(customerId) {
    localStorage.removeItem(customerId);
    console.log(localStorage.getItem(customerId))
    try {
        displayCustomerProducts(customerId)
    }
    catch (e) {
        console.log("No Element");
    }
}


function deleteFromTheCart(customerId, productId) {

    const storageData = localStorage.getItem(customerId);

    if (storageData) {
        const customerData = JSON.parse(storageData);

        const updatedData = customerData.filter(item => item.productId !== productId);

        localStorage.setItem(customerId, JSON.stringify(updatedData));
    }
    console.log(localStorage.getItem(customerId))

    try {
        displayCustomerProducts(customerId)
    }
    catch (e) {
        console.log("No Element");
    }
}



function decreaseQuantity(customerId, productId) {

    const storageData = localStorage.getItem(customerId);

    if (storageData) {

        const customerData = JSON.parse(storageData);


        const productIndex = customerData.findIndex(item => item.productId === productId);

        if (productIndex !== -1) {

            customerData[productIndex].quantity -= 1;

            if (customerData[productIndex].quantity === 0) {

                customerData.splice(productIndex, 1);
            }


            localStorage.setItem(customerId, JSON.stringify(customerData));
        } else {
            console.log(`The product with id = ${productId} didn't exist.`);
        }
    } else {
        console.log(`No data found for customerId: ${customerId}`);
    }
    console.log(localStorage.getItem(customerId))


    try {
        displayCustomerProducts(customerId)
    }
    catch (e) {
        console.log("No Element");
    }
}




function displayCustomerProducts(customerId) {

    const storageData = localStorage.getItem(customerId);

    if (storageData) {

        const products = JSON.parse(storageData);
        const url = 'https://localhost:44355/api/products';

        const selectedProductsDiv = document.getElementById('selected-products');


        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(products)
        };

        fetch(url, requestOptions)
            .then(response => response.json())
            .then(data => {

                selectedProductsDiv.innerHTML = '';
                console.log(data)

                if (data.value.length == 0) {
                    selectedProductsDiv.innerHTML = `

                                    <div class="d-flex justify-content-center">
                                        <div class="no-data flex-column" style="padding-top:40px;">
                                            <div class="no-data-animation"></div>
                                            <div class=" container text-center" style="
                                                                        display:flex;align-items:center;justify-content:center;color:white;
                                                                        transition:all 0.9s; margin-top:40px;margin-bottom:40px">
                                                The Cart Is Empty Now.
                                            </div>
                                        </div>

                                    </div>

                        `
                        ;
                }
                else {
                    data.value.forEach(product => {
                        const { id, name, description, price, image, quantity } = product;

                        const productElement = document.createElement('div');
                        productElement.classList.add('col-sm-12');
                        productElement.classList.add('col-md-6');
                        productElement.classList.add('mb-4');
                        productElement.classList.add('col-lg-4');
                        productElement.classList.add('card-container-h');

                        productElement.innerHTML =
                            `
                                        <div class="product-item   d-flex flex-column justify-content-between" style="min-height:100% !important;">
                                            <a href="shop-single.html" class="product-img">
                                                <span class="custom-badge sale" style="font-size:20px">Price ${price}</span>
                                                <img src="${image}" alt="Image" class="img-fluid">
                                            </a>
                                            <div class="d-flex flex-column">
                                                <span style="font-size:20px; font-weight:1000">${name}</span>
                                                <span style="font-size:20px; font-weight:600">${quantity}</span>
                                            </div>

                                        </div>

						`;

                        selectedProductsDiv.appendChild(productElement);
                    });
                }
              
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
    else {
        const selectedProductsDiv = document.getElementById('selected-products');
        selectedProductsDiv.innerHTML = `
                             <div class="d-flex justify-content-center">
                                        <div class="no-data flex-column" style="padding-top:40px;">
                                            <div class="no-data-animation"></div>
                                            <div class=" container text-center" style="
                                                                        display:flex;align-items:center;justify-content:center;color:white;
                                                                        transition:all 0.9s; margin-top:40px;margin-bottom:40px">
                                                The Cart Is Empty Now.
                                            </div>
                                        </div>

                                    </div>
                            `;

    }
}



