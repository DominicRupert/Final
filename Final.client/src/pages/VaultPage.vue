<template>

  <div class="masonry-frame">
    <KeepModal />
    <!-- <div class="row"> -->
      <!-- <div class="col-12"> -->
     <button>hello</button>
     <div v-for="k in keeps" :key="k.id">
<Keep :keep="k" />
     </div>
        <!-- <h5 class="card-title">{{ vault.keep }}</h5> -->
        <!-- <p class="card-text">{{ keep.description }}</p> -->
      <!-- </div> -->
    <!-- </div> -->
    </div>
  <!-- </div> -->
</template>


<script>
import { computed, onMounted, ref } from "@vue/runtime-core";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { Modal } from "bootstrap";
// import Pop from "../utils/Pop";
import { useRoute, useRouter } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService";
import { vaultsService } from "../services/VaultsService";
import { router } from "../router";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop";
import VaultKeep from "../components/VaultKeep.vue";
// import Keep from "../components/Keep.vue";

export default {
    name: "Vault",
    props: {
        vaultkeep: {
            type: Object,
            required: true
        },
        vault: {
            type: Object,
            required: true
        },
        props: {
            keep: {
                type: Object,
                required: true
            }
        }
    },
    setup(props) {
        const route = useRoute();
        const router = useRouter();
        onMounted(async () => {
            try {
                // await vaultsService.getVaultsByUserId(route.params.id);
                // await keepsService.getKeeps();
                // await vaultsService.getVaultById(route.params.id);
                // await vaultKeepsService.getVaultKeeps(route.params.id);
                // await keepsService.getKeepsByVaultId(route.params.id);
                await vaultKeepsService.getVaultKeepsByVaultId(route.params.id);
                await keepsService.getKeepsByVaultId(route.params.id);
                // await vaultKeepsService.getVaultKeeps(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message);
                router.push({ name: "Home" });
            }
        });
        return {
          vaultKeeps: computed(() => AppState.vaultKeeps),
          keeps: computed(() => AppState.keeps),
          vault: computed(() => AppState.vaults),
            // vaultKeeps: computed(() => { AppState.vaultKeeps }),
       
            async deleteVaults() {
                try {
                    if (await Pop.confirm()) {
                        await vaultsService.deleteVaults(AppState.activeVault);
                        Pop.toast("Vault gone");
                        router.push({
                            name: "profile",
                            params: {
                                id: props.activeVault.userId
                            }
                        });
                    }
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message);
                }
            },
            async deleteVaultKeeps() {
                try {
                    await vaultKeepsService.deleteVaultKeeps(AppState.activeVault);
                    Pop.toast("VaultKeeps gone");
                    router.push({
                        name: "Profile",
                        params: {
                            id: props.activeVault.userId
                        }
                    });
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message);
                }
            },
        };
    },
    components: { VaultKeep }
}

</script>


<style lang="scss" scoped>
</style>