<template>
    <div class="container-grid">
        <drink-list
            @selectDrink="selectDrink"
            class="drink-list"
            v-model:drinks="drinks"
            v-model:depositeMoney="depositeMoney"
        />
        <control-panel
            @putCoin="putCoin"
            @cashBack="cashBack"
            class="control-panel"
            v-model:depositeMoney="depositeMoney"
            v-model:maxCostDrink="maxCostDrink"
            v-model:coins="coins"
        />
    </div>
</template>

<script>
import DrinkList from "@/components/DrinkList.vue";
import axios from "axios";
import { CoinInfo } from "@/classes/CoinInfo.js";
import ControlPanel from "@/components/ControlPanel.vue";

export default {
    components: {
        DrinkList,
        ControlPanel,
    },

    data() {
        return {
            drinks: [],
            coins: [],
            inputedCoins: [],
            depositeMoney: 0,
            maxCostDrink: 0,
        };
    },

    computed: {
        async stockCoins() {
            return (
                await axios.get(
                    `https://localhost:7095/api/Coins/IsStock/VendingMachine/${this.$route.params.idVendingMachine}`
                )
            ).data;
        },
    },

    methods: {
        getMaxCostDrink() {
            this.maxCostDrink = Math.max(
                this.drinks.map((drink) => drink.cost)
            );
        },

        putCoin(coin) {
            this.depositeMoney += coin.nominal;
            this.inputedCoins.push(coin);
        },

        async cashBack() {
            const sortedDescCoins = (await this.stockCoins)
                .sort((a, b) => a.nominal - b.nominal)
                .reverse();

            let coinsCashBack = [];
            for (let i = 0; i < sortedDescCoins.length; i++) {
                const coin = sortedDescCoins[i];

                if (
                    this.depositeMoney <= 0 ||
                    coin.nominal > this.depositeMoney
                ) {
                    continue;
                }

                let machineCoin = (
                    await axios.get(
                        `https://localhost:7095/api/VendingMachineCoins/VendingMachine/Coin/${this.$route.params.idVendingMachine}/${coin.id}`
                    )
                ).data;

                const genCountRemoval = Math.floor(
                    this.depositeMoney / coin.nominal
                );
                let countRemoval = Math.min(machineCoin.count, genCountRemoval);

                if (
                    machineCoin.count >= genCountRemoval &&
                    this.depositeMoney % coin > 0
                ) {
                    countRemoval -= 1;
                }

                let coinCashBack = new CoinInfo(coin.nominal);
                coinCashBack.count = countRemoval;
                coinCashBack.sum = coinCashBack.nominal * countRemoval;

                machineCoin.count -= countRemoval;

                this.depositeMoney -= coinCashBack.sum;

                coinsCashBack.push(coinCashBack);
                await axios.put(
                    `https://localhost:7095/api/VendingMachineCoins/${machineCoin.id}`,
                    machineCoin
                );
            }

            let outMessage = ``;

            if (this.depositeMoney > 0) {
                outMessage += `Извините, но мы не можем выдать всю сумму сдачи\n`;
            }

            outMessage += `Ваша сдача: ${coinsCashBack.reduce(
                (a, b) => a + b.sum,
                0
            )}\n${coinsCashBack
                .map(
                    (coin) =>
                        `Номинал: ${coin.nominal}, Количество: ${coin.count}, Сумма: ${coin.sum}`
                )
                .join("\n")}`;

            alert(outMessage);
        },

        async selectDrink(drink) {
            this.depositeMoney -= drink.cost;

            const vendingMachineDrink = (
                await axios.get(
                    `https://localhost:7095/api/VendingMachineDrinks/${this.$route.params.idVendingMachine}/${drink.id}`
                )
            ).data;

            await this.updateDrinkCount(vendingMachineDrink);
            await this.updateCoinsCount(drink);
        },

        async updateDrinkCount(vendingMachineDrink) {
            vendingMachineDrink.count -= 1;

            await axios.put(
                `https://localhost:7095/api/VendingMachineDrinks/${vendingMachineDrink.id}`,
                vendingMachineDrink
            );
        },

        async updateCoinsCount(drink) {
            const vendingMachineCoins = (
                await axios.get(
                    `https://localhost:7095/api/VendingMachineCoins/VendingMachine/${this.$route.params.idVendingMachine}`
                )
            ).data;

            vendingMachineCoins.forEach(async (machineCoin) => {
                let vendingMachineCoin = this.inputedCoins.filter(
                    (inpCoin) => machineCoin.idCoin == inpCoin.id
                );

                if (vendingMachineCoin.length == 0) {
                    return;
                }

                const nominal = this.coins.find(
                    (coin) => coin.id == machineCoin.idCoin
                ).nominal;

                machineCoin.count +=
                    vendingMachineCoin.reduce((a, b) => a + b.nominal, 0) >
                    drink.cost
                        ? Math.ceil(drink.cost / nominal)
                        : vendingMachineCoin.length;

                await axios.put(
                    `https://localhost:7095/api/VendingMachineCoins/${machineCoin.id}`,
                    machineCoin
                );
            });
        },
        async getData() {
            try {
                await this.getDrinks();
                await this.getCoins();

                this.getMaxCostDrink();
            } catch (ex) {
                alert(ex);
            }
        },

        async getDrinks() {
            this.drinks = (
                await axios.get(
                    `https://localhost:7095/api/Drinks/IsStock/VendingMachine/${this.$route.params.idVendingMachine}`
                )
            ).data;
        },

        async getCoins() {
            this.coins = (
                await axios.get(
                    `https://localhost:7095/api/Coins/VendingMachine/${this.$route.params.idVendingMachine}`
                )
            ).data;
        },
    },
    created() {
        this.getData();
    },
};
</script>

<style lang="scss" scoped>
.control-panel {
    padding: 15px;
}

.container {
    &.wrapper {
        display: flex;
        align-content: flex-start;
        justify-content: flex-end;
    }
}

#rigth-panel {
    padding: 25px;
    background-color: #a39f93;
}
</style>