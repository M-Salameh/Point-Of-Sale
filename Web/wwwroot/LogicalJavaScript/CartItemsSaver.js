


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
	console.log("ddddddddddd")
	var TotalPrice = 0

	const storageData = localStorage.getItem(customerId);

	if (storageData) {

		const products = JSON.parse(storageData);
		const url = 'https://localhost:44355/api/products';

		const containerDiv = document.getElementById('containerDiv');

		const requestOptions = {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify(products)
		};

		fetch(url, requestOptions)
			.then(response => response.json())
			.then(data => {

				console.log(data)

				if (data.value.length == 0) {
					editTotalPrice(0)
					containerDiv.innerHTML = `

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
					containerDiv.innerHTML = `

					<div class="row">
							<div class="site-blocks-table">
								<table class="table table-bordered">
									<thead>
										<tr>
											<th class="product-thumbnail">Image</th>
											<th class="product-name">Name</th>
											<th class="product-price">Price</th>
											<th class="product-quantity">Quantity</th>
											<th class="product-remove">Total Price</th>
											<th class="product-remove">Edit</th>
											<th class="product-remove">Remove</th>
										</tr>
									</thead>
									<tbody id="selected-products">

									</tbody>
								</table>
							</div>
						</div>

					`;
					const selectedProductsDiv = document.getElementById('selected-products');
					selectedProductsDiv.innerHTML = '';
					data.value.forEach(product => {
						const { id, name, description, price, image, quantity } = product;
						TotalPrice += price * quantity
						editTotalPrice(TotalPrice)
						console.log("TotalPrice" + TotalPrice)
						console.log("price" + price)
						console.log("quantity" + quantity)
						const productElement = document.createElement('tr');
						//productElement.classList.add('col-sm-12');
						//productElement.classList.add('col-md-6');
						//productElement.classList.add('mb-4');
						//productElement.classList.add('col-lg-4');
						//productElement.classList.add('cart-card');

						productElement.innerHTML =

							`

						
							<td class="product-thumbnail">
								<img src="${image}" alt="Image" class="img-fluid">
							</td>
							<td class="product-name">
								<h2 class="h5 text-black">${name}</h2>
							</td>
							<td>${price}</td>
							<td>
							  ${quantity}
							</td>
							<td>${price * quantity}</td>

							<td>
									<button class="m-1 btn" onclick="addToCart(${customerId},${id})" >
										<svg xmlns="http://www.w3.org/2000/svg" width="50px" height="50px" viewBox="0 0 24 24" fill="none">

											<g id="SVGRepo_bgCarrier" stroke-width="0"/>

											<g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"/>

											<g id="SVGRepo_iconCarrier"> <path d="M7 12L12 12M12 12L17 12M12 12V7M12 12L12 17" stroke="#000000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/> <circle cx="12" cy="12" r="9" stroke="#000000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/> </g>

										</svg>
									</button>

									<button class="m-1 btn" onclick="decreaseQuantity(${customerId},${id})">
										<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="44px" height="44px" viewBox="0 -0.5 21 21" version="1.1" fill="#000000">

											<g id="SVGRepo_bgCarrier" stroke-width="0"/>

											<g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"/>

											<g id="SVGRepo_iconCarrier"> <title>minus_circle [#1442]</title> <desc>Created with Sketch.</desc> <defs> </defs> <g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"> <g id="Dribbble-Light-Preview" transform="translate(-379.000000, -520.000000)" fill="#000000"> <g id="icons" transform="translate(56.000000, 160.000000)"> <path d="M329.3,371 L337.7,371 L337.7,369 L329.3,369 L329.3,371 Z M333.5,378 C328.8674,378 325.1,374.411 325.1,370 C325.1,365.588 328.8674,362 333.5,362 C338.13155,362 341.9,365.588 341.9,370 C341.9,374.411 338.13155,378 333.5,378 L333.5,378 Z M333.5,360 C327.70085,360 323,364.477 323,370 C323,375.523 327.70085,380 333.5,380 C339.2981,380 344,375.523 344,370 C344,364.477 339.2981,360 333.5,360 L333.5,360 Z" id="minus_circle-[#1442]"> </path> </g> </g> </g> </g>

										</svg> 
									</button>
								  
							</td>
							<td>
								 <button class="m-1 btn"  onclick="deleteFromTheCart(${customerId},${id})">
										<svg xmlns="http://www.w3.org/2000/svg" fill="#000000" width="44px" height="44px" viewBox="-3.5 0 19 19" class="cf-icon-svg">

											<g id="SVGRepo_bgCarrier" stroke-width="0"/>

											<g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"/>

											<g id="SVGRepo_iconCarrier">

											<path d="M11.383 13.644A1.03 1.03 0 0 1 9.928 15.1L6 11.172 2.072 15.1a1.03 1.03 0 1 1-1.455-1.456l3.928-3.928L.617 5.79a1.03 1.03 0 1 1 1.455-1.456L6 8.261l3.928-3.928a1.03 1.03 0 0 1 1.455 1.456L7.455 9.716z"/>

											</g>

										</svg>

									</button>
							</td>



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
		editTotalPrice(0)

		containerDiv.innerHTML = `
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

function editTotalPrice(TotalPrice) {
	const totalPrice = document.getElementById('totalPrice');
	totalPrice.innerHTML = TotalPrice
	console.log(TotalPrice)
}