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

async function main(rootElement) {
	const response = await fetch("/items", { headers: { "Authorization": "Bearer secret" } })
		.then(res => {
			console.log(res)
			return res.json()
		})
	const cardBox = createCardBox()
	for (card of response.data.contents) {
		cardBox.appendChild(createCard(card))
	}
	rootElement.appendChild(cardBox)
}

function createCardBox(){
	const cardBox = document.createElement("div")
	cardBox.className = "cardBox"
	return cardBox
}

function createCard(cardData){
	const card = document.createElement("div")
	for ([key, value] of Object.entries(cardData)) {
		const row = createKeyValueRow(key, value)
		card.appendChild(row)
	}
	card.className = "card"
	return card
}

function createKeyValueRow(key, value){
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
