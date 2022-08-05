<template>
<div class="container d-flex justify-content-center">
    <KeepModal />
    <!-- <div class="row"> -->
      <!-- <div class="col-12"> -->
  <div>
  <button class="btn rounded-0 p-3 m-3 btn-danger mdi mdi-delete" @click.prevent="deleteVaults">Delete vault</button>
  </div>
</div>
  <div class="masonry-container">
  <div>
     <div v-for="k in keeps" class="p-4" :key="k.id">
<Keep :keep="k" class="" />
     </div>
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
                // if(vault.isPrivate && vault.userId !== AppState.user.id) {
                //     router.push("/");
                // }
                // await vaultsService.getVaultsByUserId(route.params.id);
                // await keepsService.getKeeps();
                // await vaultsService.getVaultById(route.params.id);
                // await vaultKeepsService.getVaultKeeps(route.params.id);
                // await keepsService.getKeepsByVaultId(route.params.id);
                await vaultKeepsService.getVaultKeepsByVaultId(route.params.id);
                await keepsService.getKeepsByVaultId(route.params.id);
                // FIXME get the vault
                await vaultsService.getVaultById(route.params.id);
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
          vaults: computed(() => AppState.vaults),
         activeVaults: computed(() => AppState.activeVault),

            // vaultKeeps: computed(() => { AppState.vaultKeeps }),
       
            async deleteVaults(vaultId) {
                try {
                    debugger;
                    if(AppState.activeVault.creator.id !== AppState.account.id) {
                        Pop.toast("You can only delete your own vaults");
                    }
                    if (await Pop.confirm()) {
                        await vaultsService.deleteVaults(route.params.id);
                        router.push({ name: "Home" });
                        Pop.toast("Vault deleted");
                 
                    }
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message);
                }
            },
            async deleteVaultKeeps(keep) {
                try {
                    if (await Pop.confirm()) {
                        await vaultKeepsService.removeVaultKeeps(keep.id);
                    Pop.toast("VaultKeeps gone");
                        
                    }
                    // router.push({
                    //     name: "Profile",
                    //     params: {
                    //         id: props.activeVault.userId
                    //     }
                    // });
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
.masonry-container {
  columns: 4;

  // column-gap: 10px;
  div {
    break-inside: avoid;
    }
   @media only screen and (max-width: 600px) {
     columns: 1;


    }
}


</style>