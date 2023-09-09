/*
type Card = {
	liveStatusOnAir: false,
	liveStatusRecording: false,
	onDemandFileName: "Djs_From_Mars_Jams_Edition.mp4",
	onDemandEncodingDescription: "MS On-Demand H.264",
	onDemandDuration: "00:04:35",
	gidEncodingProfileOnDemand: "828c3395-0303-45cd-9651-bd7226b3198a",
	liveMultibitrate: false,
	title: "test video 11",
	trash: false,
	hasPoster: false,
	onDemandEncodingStatus: 4,
	gidProperty: "15fa3bc1-760d-45bb-9eeb-1508d3a9a81d",
	contentId: "au59woOr67GX",
	contentType: 10,
	deliveryStatus: 1,
	protectedEmbed: false,
	creationDate: "2021-04-14T21:00:53.913",
	updateDate: "2021-04-14T21:04:42.95",
	publishDateUTC: "2021-04-14T21:00:53.913",
	publishStatus: 1,
	imageUrl: "https://picsum.photos/seed/picsum/500/600"
}
*/

async function createCardBoxAndSearch(filterTitle) {
	const query = filterTitle ? `?filter=${filterTitle}` : ""
	const response = await fetch(`/item${query}`, { headers: { "Authorization": "Bearer secret" } })
		.then(res => {
			console.log(res)
			return res.json()
		})
	const cardBox = createCardBox()
	for (cardData of response.data.contents) {
		const card = createCard(cardData)
		cardBox.appendChild(card)
	}
	return cardBox
}

function createCardBox() {
	const cardBox = document.createElement("div")
	cardBox.className = "cardBox"
	return cardBox
}

function createCard(cardData) {
	const cardEntries = Object.entries(cardData)
	const importantFields = ["contentId", "onDemandFileName", "onDemandEncodingDescription", "onDemandDuration"]

	const card = document.createElement("div")
	card.appendChild(createCardTitle(cardData.title))
	card.appendChild(createImage(cardData.imageUrl))

	card.appendChild(
		createCardMainData(cardEntries.filter(([key, _]) => importantFields.includes(key)))
	)

	console.log("provv")

	const flagsFields = ["liveStatusOnAir", "liveStatusRecording", "liveMultibitrate", "trash", "hasPoster", "protectedEmbed"];
	const dateFields = ["creationDate", "updateDate", "publishDateUTC",]

	card.className = "card"

	const deleteButton = createDeleteButton(cardData, card)
	card.appendChild(deleteButton)

	return card
}

function createCardTitle(titleText) {
	const title = document.createElement("div")
	title.className = "cardTitle"
	title.innerText = titleText
	return title
}

function createImage(imageUrl) {
	const imageContainer = document.createElement("div")	
	const image = document.createElement("img")
	image.src = imageUrl
	imageContainer.appendChild(image)
	imageContainer.className = "cardImage"
	return imageContainer
}


function createCardMainData(importantEntries) {
	const mainDataParagraph = document.createElement("div")
	mainDataParagraph.className = "mainData"
	importantEntries.forEach(([key, value])=>{
		const row = createKeyValueRow(key, value)
		mainDataParagraph.appendChild(row)
	})

	return mainDataParagraph
}

function createDeleteButton(cardData, card) {
	const deleteButton = document.createElement('button')
	deleteButton.innerText = "delete"
	const deleteButtonOnClick = async () => {
		const id = cardData.contentId
		const response = await fetch(
			`/item/${id}/delete`,
			{
				headers: { "Authorization": "Bearer secret" },
				method: "POST"
			}
		).then(res => res.json())
		if (response.id === id) card.remove()
	}
	deleteButton.onclick = deleteButtonOnClick
	return deleteButton
}

function createKeyValueRow(key, value) {
	const rowContainer = document.createElement("div")
	const keyCell = document.createElement("div")
	const valueCell = document.createElement("div")

	keyCell.innerHTML = key
	keyCell.className = "cell"
	valueCell.innerHTML = value
	valueCell.className = "cell"

	rowContainer.appendChild(keyCell)
	rowContainer.appendChild(valueCell)
	rowContainer.className = "row"
	return rowContainer
}

