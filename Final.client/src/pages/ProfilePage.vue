<template>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Profile Page</h1>
            </div>
        </div>

      
                            <div class="masonry-frame">
                                <h3>Keeps</h3>
                                <div class="masonry-grid">
                                    <div class="masonry-frame">
                                    <div v-for="keep in keeps" :key="keep.id">
                                    <Keep :keep="keep" />
                                    </div>
                              
                            </div>
                            </div>
                            <div class="col-md-8">
                                <router-view></router-view>
                            </div>
                        </div>
                    </div>
             



</template>


<script>
import { AppState } from '../AppState.js'
import { computed, watchEffect, ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'
import { accountService } from '../services/AccountService.js'
import { vaultsService } from '../services/VaultsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { profilesService } from '../services/ProfilesService.js'
import Pop from '../utils/Pop.js'
import Keep from '../components/Keep.vue'



export default {
    setup() {
        const route = useRoute();
        onMounted(async () => {
            try {
                await profilesService.getProfile(route.params.id);
                await keepsService.getKeepsByProfile (route.params.id);
                await profilesService.getProfileVaults(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message);
            }
        });
        return {
            profile: computed(() => AppState.activeProfile),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
            vaultKeeps: computed(() => AppState.vaultKeeps),
            profileKeeps: computed(() => AppState.profileKeeps),
            profileVaults: computed(() => AppState.profileVaults),
        };
    },
    components: { Keep }
}
</script>


<style lang="scss" scoped>

</style>