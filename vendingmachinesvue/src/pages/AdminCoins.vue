<template>
    <div class="container container__vertical">
        <div class="container">
            <div
                class="coin container container__vertical w-auto"
                v-for="coin in coins"
                :key="coin.id"
            >
                <div
                    class="
                        nominal
                        border border-2 border-warning
                        rounded-circle
                    "
                >
                    <span>{{ coin.idCoinNavigation.nominal }} руб.</span>
                </div>

                <input
                    class="border border-1 border-secondary form-control"
                    type="text"
                    v-model="coin.count"
                />

                <input
                    class="form-check-input mt-3"
                    type="checkbox"
                    id="flexCheckDefault"
                    v-model="coin.isActive"
                />
            </div>
        </div>
        <button class="btn btn-primary mt-3" @click="updateData">
            Сохранить
        </button>
    </div>
</template>

<script>
import axios from "axios";

export default {
    

    data() {
        return {
            coins: [],
        };
    },

    methods: {
        async getCoins() {
            const response = await axios.get(
                `https://localhost:7095/api/VendingMachineCoins/VendingMachine/${this.$route.params.idVendingMachine}`
            );

            this.coins = response.data;
        },

        changeActive(coin) {
            coin.isActive = !coin.isActive;
            this.updateData();
        },

        updateData() {
            this.coins.forEach(async (coin) => {
                await axios.put(
                    `https://localhost:7095/api/VendingMachineCoins/${coin.id}`,
                    coin
                );
            });

            alert("Данные успешно сохранены!");

            this.getCoins();
        },
    },

    created() {
        this.getCoins();
    },
};
</script>

<style lang="scss" scoped>
.nominal {
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: yellow;
    width: 75px;
    height: 75px;
    margin: 15px;
}
</style>