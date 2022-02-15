import ChooseVendingMachine from "@/pages/ChooseVendingMachine";
import BuyingDrink from "@/pages/BuyingDrink";
import AdminPanel from "@/pages/AdminPanel";
import AdminCoins from "@/pages/AdminCoins";
import AdminDrinks from "@/pages/AdminDrinks";
import AdminReport from "@/pages/AdminReport";
import ChoosingDirectory from "@/pages/ChoosingDirectory";
import DrinkEdit from "@/pages/DrinkEdit";
import { createRouter, createWebHistory } from "vue-router";

const routes = [
    {
        path: '/',
        component: ChooseVendingMachine
    },
    {
        path: '/:idVendingMachine',
        component: BuyingDrink
    },
    {
        path: '/admin',
        component: AdminPanel
    },
    {
        path: '/admin/:idVendingMachine/chooseDirectory',
        component: ChoosingDirectory
    },
    {
        path: '/admin/:idVendingMachine/chooseDirectory/coins',
        component: AdminCoins
    },
    {
        path: '/admin/:idVendingMachine/chooseDirectory/drinks',
        component: AdminDrinks
    },
    {
        path: '/admin/:idVendingMachine/chooseDirectory/report',
        component: AdminReport
    },
    {
        path: '/admin/:idVendingMachine/chooseDirectory/drinks/:idDrink/drinkEdit',
        component: DrinkEdit
    },
]

const router = createRouter({
    routes,
    history: createWebHistory(process.env.BASE_URL)
});

export default router;