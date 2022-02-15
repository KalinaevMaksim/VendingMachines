<template>
	<div class="container container__vertical">
		<h1>Выберите автомат</h1>
		<div class="container">
			<vending-machine
				v-for="vendingMachine in vendingMachines"
				:key="vendingMachine.id"
				:vendingMachine="vendingMachine"
				:url="`/${vendingMachine.id}`"
			/>
		</div>
	</div>
</template>

<script>
import axios from "axios";
import VendingMachine from "@/components/VendingMachine.vue";

export default {
	components: { VendingMachine },
	data() {
		return {
			vendingMachines: [],
		};
	},

	methods: {
		async getVendingMachines() {
			try {
				const request = await axios.get(
					"https://localhost:7095/api/VendingMachines"
				);
				this.vendingMachines = request.data;
			} catch (ex) {
				alert(ex);
			}
		},
	},

	created() {
		this.getVendingMachines();
	},
};
</script>

<style lang="scss" scoped>
</style>