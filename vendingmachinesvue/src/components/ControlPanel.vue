<template>
    <div class="control-panel container container__vertical border border-2 border-dark">
        <div class="deposite-money border rounded-15">
            <span>{{ depositeMoney }} руб.</span>
        </div>

        <div class="input-panel container-grid container-grid__2">
            <button
                class="coin-item btn btn-secondary"
                v-for="coin in coins"
                :key="coin"
                :disabled="lockCoin(coin) == false"
                @click="putCoin(coin)"
            >
                {{ coin.nominal }}
            </button>
        </div>

        <div class="instruction container w-auto container__vertical border border-2 border-dark">
            <label>1.Внесите деньги</label>
            <label>2.Выберите напиток</label>
            <label>3.Получите сдачу</label>
        </div>

        <div class="change">
            <button @click="cashBack" class="btn btn-primary p-3">Сдача</button>
        </div>
    </div>
</template>

<script>
import axios from "axios";
export default {
    props: {
        depositeMoney: 0,
        maxCostDrink: 0,
        coins: {
            type: Array
        },
    },

    data() {
        return {
            activeCoins: [],
        };
    },

    methods: {
        async getActiveCoins() {
            this.activeCoins = (
                await axios.get(
                    `https://localhost:7095/api/Coins/Active/VendingMachine/${this.$route.params.idVendingMachine}`
                )
            ).data;
        },

        putCoin(coin) {
            this.$emit("putCoin", coin);
        },

        lockCoin(coin) {
            return this.activeCoins.some(
                (actCoin) => actCoin.id == coin.id
            );
        },

        cashBack() {
            this.$emit("cashBack");
        },
    },

    mounted() {
        this.getActiveCoins();
    },
};
</script>


<style lang="scss" scoped>
.control-panel {
    justify-content: space-around;
    background: lightgray;
}

.deposite-money {
    background-color: #39515c;
    padding: 15px 0px;
    text-align: center;
    width: 90%;
    margin: 0px 0px 30px 0px;

    span {
        color: white;
    }

    &.border {
        border: 0px;
    }
}

.input-panel {
    width: 70%;
    max-width: 150px;
    margin: 0px 0px 30px 0px;

    button {
        padding: 40% 0px;
        margin: 2px;
    }
}

.instruction {
    align-items: flex-start;
    margin: 0px 0px 30px 0px;

}
</style>